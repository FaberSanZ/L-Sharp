// This code has been based from the sample repository "cecil": https://github.com/jbevain/cecil
// Copyright (c) 2020 - 2021 Faber Leonardo. All Rights Reserved. https://github.com/FaberSanZ
// This code is licensed under the MIT license (MIT) (http://opensource.org/licenses/MIT)



using System;
using System.IO;

namespace LSharp.IL
{

	public sealed class EmbeddedResource : Resource {

		readonly MetadataReader reader;

		uint? offset;
		byte [] data;
		Stream stream;

		public override ResourceType ResourceType {
			get { return ResourceType.Embedded; }
		}

		public EmbeddedResource (string name, ManifestResourceAttributes attributes, byte [] data) :
			base (name, attributes)
		{
			this.data = data;
		}

		public EmbeddedResource (string name, ManifestResourceAttributes attributes, Stream stream) :
			base (name, attributes)
		{
			this.stream = stream;
		}

		internal EmbeddedResource (string name, ManifestResourceAttributes attributes, uint offset, MetadataReader reader)
			: base (name, attributes)
		{
			this.offset = offset;
			this.reader = reader;
		}

		public Stream GetResourceStream ()
		{
			if (stream != null)
				return stream;

			if (data != null)
				return new MemoryStream (data);

			if (offset.HasValue)
				return new MemoryStream (reader.GetManagedResource (offset.Value));

			throw new InvalidOperationException ();
		}

		public byte [] GetResourceData ()
		{
			if (stream != null)
				return ReadStream (stream);

			if (data != null)
				return data;

			if (offset.HasValue)
				return reader.GetManagedResource (offset.Value);

			throw new InvalidOperationException ();
		}

		static byte [] ReadStream (Stream stream)
		{
			int read;

			if (stream.CanSeek) {
				var length = (int) stream.Length;
				var data = new byte [length];
				int offset = 0;

				while ((read = stream.Read (data, offset, length - offset)) > 0)
					offset += read;

				return data;
			}

			var buffer = new byte [1024 * 8];
			var memory = new MemoryStream ();
			while ((read = stream.Read (buffer, 0, buffer.Length)) > 0)
				memory.Write (buffer, 0, read);

			return memory.ToArray ();
		}
	}
}
